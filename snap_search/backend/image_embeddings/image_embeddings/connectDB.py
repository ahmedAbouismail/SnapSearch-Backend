from flask import Flask, request
from flask_mysqldb import MySQL
import yaml

app = Flask(__name__)

# Configure db
db = yaml.load(open('db.yaml'))
app.config['MYSQL_HOST'] = db['mysql_host']
app.config['MYSQL_USER'] = db['mysql_user']
app.config['MYSQL_PASSWORD'] = db['mysql_password']
app.config['MYSQL_DB'] = db['mysql_db']

mysql = MySQL(app)

@app.route('/', methods=["GET", "POST"])
def index():
    if request.method == "GET":
        PhotoId = 1
        PhotoName = "flower"
        PhotoUrl = "local/flower"
        PhotoResult = 0.355
        #create curser to accese the db
        cur = mysql.connection.cursor()
        #add to the db
        cur.execute("INSERT INTO result(PhotoId, PhotoName, PhotoUrl, PhotoResult) VALUES(%s, %s, %s, %s)",
        (PhotoId, PhotoName, PhotoUrl, PhotoResult))
        #save the changes 
        mysql.connection.commit()
        #close the connection
        cur.close()
        return "Done"

if __name__ == "__main__":
    app.run()
