from flask import Flask, request
from flask_mysqldb import MySQL
import yaml

app = Flask(__name__)

# Configure db
db = yaml.load(open("db.yaml"))
app.config['MYSQL_HOST'] = db['mysql_host']
app.config['MYSQL_USER'] = db['mysql_user']
app.config['MYSQL_PASSWORD'] = db['mysql_password']
app.config['MYSQL_DB'] = db['mysql_db']

mysql = MySQL(app)

@app.route('/populate')
def populate_db():
    PhotoId = 5
    PhotoName = "gnflkgrlnk"
    PhotoUrl = "local/flower"
    #create curser to accese the db
    print(mysql.connection.cursor())
    cur = mysql.connection.cursor()
    #add to the db
    cur.execute("INSERT INTO result(PhotoId, PhotoName, PhotoUrl) VALUES(%s, %s, %s)",
    (PhotoId, PhotoName, PhotoUrl))
    #save the changes 
    mysql.connection.commit()
    #close the connection
    cur.close()
    return "Done"

if __name__ == "__main__":
    app.run(debug=True)
