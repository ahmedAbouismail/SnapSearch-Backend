# Snabearch Backend

Snapserch is a CBIR search app
## Installation

Use the package manager ```pipenv```to install dependances

```bash
cd /team9/snap_search/backend/image_embeddings
```

```bash
pipenv install
```
## Usage
Run algorithm on the database images

```python
pipenv run python setup.py
```

Run the server

```python
export FLASK_APP=server.py
```
```python
python -m flask run
```


#### Endpoints:
```http://127.0.0.1:5000/uploadimage/<int:result_num>```  Where ```/<int:result_num>``` is the amount of the photos needed in the result

#### Client server use 
Use the endpoint above to send a ```post``` request.
The image-to-search-with should be sent in the body with ```form-data```

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
```http://127.0.0.1:5000/populate```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License

[License](https://gitlab.rz.htw-berlin.de/softwareentwicklungsprojekt/wise2020-21/team9/-/blob/master/LICENSE)