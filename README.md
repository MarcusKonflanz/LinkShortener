
# Encurtador de links

### Português pt-BR
## Documentação da API

#### Encurta a URL Original

```http
  Post /api/Shortener
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `url original` | `string` | **Obrigatório**. URL que será encurtado |

#### Redireciona a URL com base no código curto

```http
  GET /api/Shortener/r/{shortCode}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `string` | **Obrigatório**. Código encurtado, redireciona de forma automática para o link |




## Autores

- [@MarcusKonflanz](https://github.com/MarcusKonflanz)


## Stack utilizada

**Back-end:** C#, .Net 8, Entitty Framework, SQLServer

## Atualizações futuras
- Sistema de login
- Busca de todos os links gerador pelo usuário
- Painel ADM com informações relevantes a geração de links
- Timer de expiração de link

