<div align="center">

<!-- ═══════════════════════════════════════════════════════════ -->
<!--                        BANNER                              -->
<!--   Substitua a linha abaixo pela sua imagem de banner       -->
<!-- ═══════════════════════════════════════════════════════════ -->

<img src="assets/banner.png" alt="TekigCloud Banner" width="100%" />

<br/>
<br/>

# ☁️ TekigCloud

### Seu armazenamento privado, no seu servidor.

<br/>

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-Service-0078D4?style=for-the-badge&logo=windows&logoColor=white)

<br/>

![GitHub repo size](https://img.shields.io/github/repo-size/SEU_USUARIO/TekigCloud?color=6c63ff&style=flat-square)
![GitHub last commit](https://img.shields.io/github/last-commit/SEU_USUARIO/TekigCloud?color=6c63ff&style=flat-square)
![License](https://img.shields.io/github/license/SEU_USUARIO/TekigCloud?color=6c63ff&style=flat-square)

</div>

---

## 📖 Sobre o projeto

**TekigCloud** é uma nuvem privada auto-hospedada — um Google Drive pessoal que roda diretamente no seu PC. Você tem controle total dos seus arquivos, sem depender de serviços de terceiros, sem mensalidade e sem limite de privacidade.

Feito com **ASP.NET Core 8**, banco **SQLite** e um frontend moderno em HTML/CSS/JS puro. Roda como **Windows Service** em segundo plano.

---

## ✨ Funcionalidades

| Recurso | Descrição |
|---|---|
| 🔐 **Autenticação JWT** | Registro e login seguros com tokens JWT |
| ☁️ **Upload de arquivos** | Suporte a arquivos de até **10 GB** |
| ⬇️ **Download** | Baixe seus arquivos a qualquer momento |
| 🔗 **Links de compartilhamento** | Gere links temporários com expiração configurável |
| 🗑️ **Gerenciamento** | Delete arquivos diretamente pela interface |
| 📊 **Dashboard** | Veja estatísticas de uso em tempo real |
| 🖱️ **Drag & Drop** | Arraste arquivos direto para o navegador |
| 🪟 **Windows Service** | Roda em segundo plano automaticamente |

---

## 🖥️ Screenshots

<!-- ═══════════════════════════════════════════════════════════ -->
<!--              Adicione seus screenshots abaixo              -->
<!-- ═══════════════════════════════════════════════════════════ -->

<div align="center">

<table>
  <tr>
    <td align="center">
      <img src="assets/screenshot-login.png" width="420px" alt="Tela de Login" />
      <br/>
      <sub><b>Tela de Login</b></sub>
    </td>
    <td align="center">
      <img src="assets/screenshot-dashboard.png" width="420px" alt="Dashboard" />
      <br/>
      <sub><b>Dashboard</b></sub>
    </td>
  </tr>
  <tr>
    <td align="center">
      <img src="assets/screenshot-upload.png" width="420px" alt="Upload de arquivos" />
      <br/>
      <sub><b>Upload de arquivos</b></sub>
    </td>
    <td align="center">
      <img src="assets/screenshot-share.png" width="420px" alt="Link de compartilhamento" />
      <br/>
      <sub><b>Link de compartilhamento</b></sub>
    </td>
  </tr>
</table>

</div>

---

## 🎬 Demonstração

<!-- ═══════════════════════════════════════════════════════════ -->
<!--         Cole aqui o link do seu vídeo no YouTube           -->
<!--   ou substitua pela tag <video> com seu arquivo local      -->
<!-- ═══════════════════════════════════════════════════════════ -->

<div align="center">

[![Assistir demonstração](assets/thumbnail-video.png)](https://www.youtube.com/watch?v=SEU_VIDEO_AQUI)

<sub>▶️ Clique na imagem para assistir a demonstração completa</sub>

</div>

---

## 🛠️ Tecnologias

- **Backend:** ASP.NET Core 8, Entity Framework Core, SQLite
- **Autenticação:** JWT Bearer Token + BCrypt
- **Frontend:** HTML5, CSS3, JavaScript puro
- **Infraestrutura:** Windows Service, Kestrel
- **Exposição externa:** Cloudflare Tunnel

---

## 🚀 Como rodar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- Windows 10/11

### Instalação

```bash
# Clone o repositório
git clone https://github.com/SEU_USUARIO/TekigCloud.git

# Entre na pasta
cd TekigCloud

# Restaure as dependências
dotnet restore

# Rode a aplicação
dotnet run
```

Acesse em: `https://localhost:7078`

### Configuração de armazenamento

No arquivo `appsettings.json`, defina onde os arquivos serão salvos:

```json
{
  "Storage": {
    "RootPath": "D:\\TekigCloud"
  }
}
```

---

## 🌐 Expondo para a internet (Cloudflare Tunnel)

Para acessar sua nuvem de qualquer lugar gratuitamente:

```bash
# Baixe o cloudflared em https://developers.cloudflare.com/cloudflare-one/connections/connect-networks/downloads/

# Rode o tunnel apontando para sua API
cloudflared.exe tunnel --url http://localhost:5000
```

O terminal vai exibir um link público `https://xxxx.trycloudflare.com` — é só acessar!

---

## 📁 Estrutura do projeto

```
TekigCloud/
├── Controllers/
│   ├── AuthController.cs      # Registro e login
│   └── FilesController.cs     # CRUD de arquivos + compartilhamento
├── Data/
│   └── AppDbContext.cs        # Contexto do banco de dados
├── Models/
│   ├── User.cs                # Modelo de usuário
│   └── FileItem.cs            # Modelo de arquivo
├── wwwroot/
│   └── index.html             # Frontend completo
├── appsettings.json
└── Program.cs                 # Configuração da aplicação
```

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

<div align="center">

Feito por **TheTekig**

</div>
