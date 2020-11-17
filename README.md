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

Endpoints:
```http://127.0.0.1:5000/uploadimage/<int:result_num>```  Where ```/<int:result_num>``` is the amount of the photos needed in the result

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License

Copyright <2020> <SINAN TUTAN, AHMED ABOUISMAIL, ABDELRAHMAN MOHAMED>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.