**VERSÃO LINUX:** ubuntu-24.04-live-server-amd64  
**VERSÃO DO .NET:** 3.1.426  
**VERSÃO DO RUNTIME .NET:** Microsoft.AspNetCore.App 3.1.32 | Microsoft.NETCore.App 3.1.32  
**VIRTUALIZADOR:** HYPER-V  
**VERSÃO:** BETA v1.0.0  
**DESENVOLVEDOR:** Gianluca Vialli Ribeiro

---

## INSTALAÇÃO .NET SDK E RUNTIME 3.1

- Para a versão 24.04 do ubuntu server, a instalação do .NET requer que duas bibliotecas sejam instaladas manualmente. Baixe as libs `libssl1.1` e `libicu66` manualmente utilizando:

    ```bash
    wget http://archive.ubuntu.com/ubuntu/pool/main/o/openssl/libssl1.1_1.1.1f-1ubuntu2_amd64.deb
    wget http://archive.ubuntu.com/ubuntu/pool/main/i/icu/libicu66_66.1-2ubuntu2_amd64.deb
    ```

- Após baixar os pacotes, instale-os manualmente com:

    ```bash
    sudo dpkg -i libssl1.1_1.1.1f-1ubuntu2_amd64.deb
    sudo dpkg -i libicu66_66.1-2ubuntu2_amd64.deb
    ```

- Com isso, instale o SDK e runtime do .NET 3.1:

    ```bash
    sudo apt update
    sudo apt install dotnet-sdk-3.1
    sudo apt install dotnet-runtime-3.1
    ```

- Confira as instalações:

    ```bash
    dotnet --list-sdks
    dotnet --list-runtimes
    which dotnet  # saber qual o diretório de instalação do dotnet
    ```

- Adicione o caminho ao PATH caso o terminal não consiga encontrar o dotnet:

    ```bash
    export PATH=$PATH:/usr/share/dotnet
    source ~/.bashrc
    sudo ln -s /usr/share/dotnet/dotnet /usr/local/bin/dotnet  # cria o link direto para o uso da ferramenta dotnet
    ```

- Confira as instalações novamente:

    ```bash
    dotnet --list-sdks
    dotnet --list-runtimes
    which dotnet  # saber qual o diretório de instalação do dotnet
    ```

---

## INSTALAÇÃO DE LIBS PARA FUNCIONAMENTO DO FAST REPORT (FERRAMENTA DE GERAÇÃO DOS RELATÓRIOS) NO LINUX

```bash
sudo apt-get install -y libgdiplus
sudo apt-get install -y libc6-dev libx11-dev
sudo apt-get install -y libfreetype6 libxrender1
sudo apt-get install -y libxtst6 libxrandr2
sudo apt-get install -y libxcb1 libx11-xcb1 libxcomposite1  libxcursor1
sudo apt-get install -y fontconfig
```

---

## INSTALAÇÃO DO MYSQL SERVER

```bash
sudo apt update
sudo apt upgrade
sudo apt install mysql-server
sudo mysql_secure_installation # para configuração segura do mysql-server
sudo nano /etc/mysql/mysql.conf.d/mysqld.cnf # para abrir o arquivo de configurações do mysql-server
```
---

- Na sessão mysqld altere ou acrescente caso não tenha:

```plaintext
Port  = 33600 # porta para execução do mysql
bind-address = 127.0.0.1 # garantir apenas comunicação local
```
---

- Reinicie o serviço do mysql:

```bash
sudo systemctl restart mysql
```

## DEFINIR SENHA SEGURA PARA USUÁRIO ROOT DO MYSQL

```bash
sudo mysql -u root -p -P 33600 /*  Primeiro acesso sem senha*/
```

```bash
ALTER USER 'root'@'localhost' IDENTIFIED BY 'sua_senha';
```

---

## CONFIGURAÇÃO DE USUÁRIO COM PERMISSÕES ESPECÍFICAS DE ACESSO PARA DETERMINADO BANCO DE DADOS

```bash
CREATE USER 'hsf.connection'@'127.0.0.1' IDENTIFIED BY 'Root@123456';
```

```bash
GRANT ALL PRIVILEGES ON *.* TO 'hsf.connection'@'127.0.0.1';
```

```bash
FLUSH PRIVILEGES; # aplica as mudanças de privilégios
```

```bash
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'hsf.connection'@'127.0.0.1'; # Revoga todos os privilégios deste usuário para que possamos configurar sua permissão apenas para o banco que irá acessar
```

```bash
GRANT USAGE ON *.* TO 'hsf.connection'@'127.0.0.1'; # garante que o usuário tenha permissão para acessar o mysql já que todos os privilégios foram revogados
```

```bash
GRANT ALL PRIVILEGES ON hsf_db.* TO 'hsf.connection'@'127.0.0.1'; # garante as permissões necessárias para o usuário dentro de um banco específico
```

```bash
FLUSH PRIVILEGES; # aplica as mudanças de privilégios
```

```bash
SELECT User, Host FROM mysql.user; # Verificar usuários criados
```

```bash
SHOW GRANTS FOR 'usuario'@'host'; # Verificar permissões para determinado usuário
```

## CONFIGURAÇÃO DO SERVIÇO PARA GERENCIAMENTO DE INICIALIZAÇÃO DA APLICAÇÃO HSF_Receitas

O serviço configurado utilizará o próprio kernel do .NET como servidor web para lidar com as requisições HTTP. Portanto, não estaremos configurando um servidor web completo, mas sim um serviço responsável por iniciar o kernel, permitindo que a aplicação seja executada sob ele.

## 1. Configuração do Diretório e Permissões

1. Por motivos de padronização, crie o diretório `www` dentro de `/var`:
    ```bash
    cd /var
    mkdir www
    ```

2. Crie um grupo para definir permissões especiais no diretório `/var/www`:
    ```bash
    chown www-data:www-data /var/www && chmod 775 /var/www
    ```

3. Com o diretório criado e as permissões aplicadas, publique os arquivos da aplicação no diretório `/var/www`:
    ```bash
    dotnet publish -o /var/www
    ```

## 2. Criando e Configurando o Arquivo de Serviço com Systemd

1. Crie o arquivo `hsf.service` dentro do diretório `/etc/systemd/system` com o comando:
    ```bash
    sudo nano /etc/systemd/system/hsf.service
    ```

2. Adicione as seguintes configurações ao arquivo:

    ```ini
    [Unit]
    Description=HSF_Receitas - Programa de Receituário do Hospital São Francisco de Três Marias     # Descrção para o serviço         
    After=network.target    # Garante que o serviço será iniciado somente depois que todos os componentes e recursos de rede do sistema estiverem prontos e devidamente inicializados

    [Service]
    WorkingDirectory=/var/www   # Define o diretório de trabalho do serviço
    ExecStart=/usr/local/bin/dotnet /var/www/Hsf_Receitas.dll   # Comando de inicialização da aplicação
    Restart=Always  # Reinicia o serviço automaticamente em caso de falha
    RestartSec=10
    KillSignal=SIGINT   # Sinal para encerramento do serviço
    SyslogIdentifier=hsf-log    # Define o identificador de logs
    User=hsf    # Usuário sob o qual o serviço será executado
    Environment=ASPNETCORE_ENVIRONMENT=Production   # Variável de ambiente
    Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false    # Variável de ambiente
    Environment="ASPNETCORE_URLS=https://0.0.0.0:5001;http://0.0.0.0:5000"  # Variável de ambiente

    [Install]
    WantedBy=multi-user.target  # Define que o serviço é inicializado com o sistema operacional
    ```

3. Salve as alterações com `CTRL + O` e feche o editor com `CTRL + X`.

4. Utilize o `daemon-reload` para incluir o serviço configurado:
    ```bash
    sudo systemctl daemon-reload
    ```

5. Habilite o serviço para inicialização automática, criando um link para a inicialização no boot:
    ```bash
    sudo systemctl enable hsf.service
    ```

6. Por fim, inicie o serviço com:
    ```bash
    sudo systemctl start hsf.service
    ```