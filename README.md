# Como correr la aplicación

## Configuraciones

Se debe editar el archivo appsettings.json y agregar o editar las siguientes configuraciones:

```json
{
  "InvoicesDatabase": {
    "ConnectionString": "<cadena_de_conexión>",
    "DatabaseName": "Invoices",
    "CollectionName": "Invoices"
  }
}
```

Reemplazar ```<cadena_de_conexión>``` por la cadena de conexión de su base de datos de mongo. Necesita crear una base de
datos y una colección para almacenar las facturas. Si la base de datos y la colección tienen nombres diferentes a los
configurados, debe cambiarlos para que los valores coincidan.

``Nota: Para fines prácticos, se puede importar el archivo data.json a mongo db. Este archivo contiene una lista de facturas.``

```json
{
  "EmailSettings": {
    "Host": "<host>",
    "Port": "<port>",
    "Username": "<username>",
    "Password": "<password>"
  }
}
```

Reemplazar ```<host>, <port>, <username>, <password>``` por los valores de su servidor de correo.

Si el servicio fronten se despliega en un servidor o un puerto distinto al por defecto debe cambiar la siguiente
configuración:

```json
{
  "AllowedUrls": [
    "http://localhost:4200"
  ]
}
```

Puede agregar varias url admitidas o editar la actual.


---

Una vez hechas todas las configuraciones puede correr la aplicación desde su IDE de preferencia. Debe tener seleccionado
como projecto de inicio "Invoices.Api". También puede correr la aplicación desde la terminal con el comando:

```bash
dotnet run --project apps/Invoices.Api/Invoices.Api.csproj
```
