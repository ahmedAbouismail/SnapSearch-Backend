import yaml
from flask_mysqldb import MySQL
from flask import Flask, request

  
def configureDbServer(app):
    """
    to set the config for the app and return back the configuierd app

    params:
    app : the current app
    """
    #open and load db.yaml in db
    db = yaml.load(open("db.yaml"))
    #db["key"] to get the value
    app.config['MYSQL_HOST'] = db['mysql_host']
    app.config['MYSQL_USER'] = db['mysql_user']
    app.config['MYSQL_PASSWORD'] = db['mysql_password']
    app.config['MYSQL_DB'] = db['mysql_db']
    print("___________________")
    print(app)
    return app

def connectMySQLServer(app):
    """
    to conennect to the Mysql Server after configuring it

    params:
    app : the current app
    """
    db = configureDbServer(app)
    mysql = MySQL(db)
    if  mysql != None:
        return mysql
    else:
        print("Bound to MySQL was unsuccessful") 

def openCursor(mysql):
    """
    create curser to accese the db

    params
    mysql : current opend mysql

    return->
    cur new Cursor each time we call the func for the same db     
    """
    cur = mysql.connection.cursor()
    if cur != None:
        return cur
    else:
        return "Error while opening new cursor"

    

def insertInDb(photoId ,photoName,photoUrl, cur):
    """
    to write the data in the db
    params->
    cur: for the current cursor
    return->
    true: if successfuly inserted
    false: if there is pronlem while inserting 
    """
    cursor = cur.execute("INSERT INTO result(PhotoId, PhotoName, PhotoUrl) VALUES(%s, %s,%s)"
    ,(photoId, photoName, photoUrl))
    if cursor != None:
        return True
    else:
        return False

def selectFromDb(photoName, cur):
    """
    to select the data from the db according to the photoName
    """
    resultValue = cur.execute(f"SELECT PhotoUrl FROM result WHERE PhotoName = '{photoName}'")
     #if PhotoUrl found? resultValue will be bigger than 0 
    if resultValue > 0:
        #cur.fetchone() to fetch the result that we found
        #PhotoNameFromDb is string with a lot special characters
        PhotoNameFromDb = str(cur.fetchone())

        return PhotoNameFromDb

def deleteFromDb(id, cur):
    """
    to delete the data from the db

    parmas->
    id: id for thr photo that we want delelte
    if = 0 -> the all rows well be removed
    """
    if id == 0:
            resultValue = cur.execute("DELETE FROM result")
            return "all"
    else:
        resultValue = cur.execute(f"DELETE FROM result WHERE PhotoId = '{id}'")
        return f"one"
def save(mysql):
    """
    to commit the changes in the db

    params
    mysql : current opend mysql
    """
    #commit the changes in db
    mysql.connection.commit()

def closeConnection(cur):
    """
    to colse the connection of the current cursor
    
    """
    #close the connection
    cur.close()