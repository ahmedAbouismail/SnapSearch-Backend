# SnapSearch Backend

Snapserch is a CBIR search app

## Requirements

- Docker

## Installation

#### Docker for Windows

- Download Docker Desktop:
  [Downolad Docker Installer .exe:](https://desktop.docker.com/win/stable/Docker%20Desktop%20Installer.exe)

- Install Docker Desktop:
  Open the `.exe` file and follow the instructions to install Docker Desktop
  Note: After installation is done, Do NOT reboot

- Download the Linux kernel update package:
  [Downolad Linux kernel .msi](https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi)

- Install Linux kernel update package:
  Open the `.msi` file and follow the instructions to install Linux kernel
  After installation is done, Reboot your machine

- Run Docker:

1. Open Docker Desktop
2. Wait until Docker runs
3. Make sure docker is running in the task bar
   ![Docker running](dockerSign.png)

## Usage

Open

```bash
cd /team9/snap_search/backend/image_embeddings
```

Build and Run Docker image for the server

```bash
docker build -t image .
```

```bash
docker run -p 5000:5000 image
```

#### Endpoints:

`http://127.0.0.1:5000/createrecords` Runs algorithm on the database images
`http://127.0.0.1:5000/uploadimage/<int:result_num>` Where `/<int:result_num>` is the amount of the photos needed in the result

#### Client server use

- `/createrecords`
  This endpoint expects `GET` request.
  After the request is sent, The algorithm runs on the database images

- `/uploadimage/<int:result_num>`
  This endpoint expects `POST` request.
  The image-to-search-with should be sent in the body with `form-data`
  The key is `input_img`

#### Client server use

Use the endpoint above to send a `post` request.
The image-to-search-with should be sent in the body with `form-data`

## Database

The Db (cbir_resualt) contains only one table (result)

### Mysql Dump

Use cbir_result_dump.sql to get the dump of the Db

```bash
cd /team9/snap_search/Database
```

## Test

To test and populate the result table you can use post_man

To put the Data in the result table change the values of the following variables

```bash
PhotoId = int (The main id of the foto from the CBIR_Algo)
```

```bash
PhotoName = string (The of the Photo)
```

```bash
PhotoUrl = string (Url to call the Photo from cloud)
```

## Connection

### Db Configuration:

The configurations are saved in db.yaml file

```bash
mysql_host: 'localhost'
```

```bash
mysql_user: 'Ahmed'
```

```bash
mysql_password: '' Without Password
```

```bash
mysql_db: 'cbir_result'
```

## Endpoints for Get Requests via post man

`http://127.0.0.1:5000/populate`

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[License](https://gitlab.rz.htw-berlin.de/softwareentwicklungsprojekt/wise2020-21/team9/-/blob/master/LICENSE)
