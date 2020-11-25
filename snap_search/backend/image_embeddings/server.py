from image_embeddings.inference import write_tfrecord
from image_embeddings.inference import run_inference
from image_embeddings.knn import get_results
from flask import Flask, request, jsonify
from PIL import Image 
import os
import io
#for mysql
from flask_mysqldb import MySQL
import yaml

app = Flask(__name__)

@app.route('/uploadimage/<int:result_num>', methods=['GET','POST'])
def process_image(result_num):
    file = request.files['input_img']
    img = Image.open(file.stream)
    currentDir = os.path.dirname(__file__)
    img.save('search.jpeg')
    os.replace(f'{currentDir}/search.jpeg', f'{currentDir}/tf_input_image/search.jpeg')
    write_tfrecord ('tf_input_image', 'tf_flower_tf_records')
    run_inference('tf_flower_tf_records', 'tf_flower_embeddings')
    result = get_results('tf_flower_embeddings', result_num)
    return result

###############################################################
# Configure db
db = yaml.load(open('db.yaml'))
app.config['MYSQL_HOST'] = db['mysql_host']
app.config['MYSQL_USER'] = db['mysql_user']
app.config['MYSQL_PASSWORD'] = db['mysql_password']
app.config['MYSQL_DB'] = db['mysql_db']

mysql = MySQL(app)

#populate_db with the 1000 Pics
@app.route('/populate')
def populate_db():
    
        PhotoId = 1
        PhotoName = "flower"
        PhotoUrl = "local/flower"
        PhotoResult = 0.355
        #create curser to accese the db
        cur = mysql.connection.cursor()
        #add to the db
        cur.execute("INSERT INTO Result(PhotoId, PhotoName, PhotoUrl, PhotoResult) VALUES(%s, %s, %s, %s)",
        (PhotoId, PhotoName, PhotoUrl, PhotoResult))
        #save the changes 
        mysql.connection.commit()
        #close the connection
        cur.close()
        return "Done"

@app.route("/reults/<int[]:ids>")
def results_db():
    cur = mysql.connection.cursor()
    
    for id in ids:
        resultValue = cur.execute("SELECT * FROM Result WHERE PhotoId = '%s' ",id)

    if resultValue > 0:
        resultDetails = cur.fetchall()

if __name__ == "_main_":

    app.run(debug=True)


