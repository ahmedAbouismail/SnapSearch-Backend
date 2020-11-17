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
## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License

[License](https://gitlab.rz.htw-berlin.de/softwareentwicklungsprojekt/wise2020-21/team9/-/blob/master/LICENSE)