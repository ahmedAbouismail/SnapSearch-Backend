from flask import Flask, request, redirect, url_for
from flask_mysqldb import MySQL
import json
from help import *
from db import *


app = Flask(__name__)



mysql = connectMySQLServer(app)
#cur = openCursor(mysql) 


@app.route("/populate/<int:photoId>/<string:photoName>/<path:photoUrl>", methods=["GET", "POST"])
def populate_db(photoId,photoName,photoUrl):
    """
    populate_db is to wirte the data in db
    the data are in the Url as params

    params->
    photoId: photoId from the Algo
    photoName: name of each photo
    photoUrl: (route)/photoName/photoId

    return-> 
    string message if the data successfuly or not
    """
    if request.method == "POST":
        #to return the status if the process done or not 
        msg = ""
        try:
            #create curser to accese the db
            #cur = openCursor(mysql)
            cur = mysql.connection.cursor()        
            if areParamsCorrect(photoId, photoName,photoUrl):
                isInserted = insertInDb(photoId, photoName, photoUrl, cur)
                if isInserted != None:
                    msg = "The data successfuly inserted"
                else:
                    msg = "Error While inserting in the db"
            else:
                msg = "Coudnt't insert the data because of wrong Param"
        except Exception  as e:
            print(e)
        finally:
            save(mysql)
            closeConnection(cur)
            return msg
      
@app.route("/reults", methods=["GET", "POST"])
def results_db():
    """
    results_db to query the db and get the wanted data back
    results_db will get jsonObj passed in the body request
    and will reurn data where photoName = Key in the jsonObj

    return->
    jsonObj: if the data founded
    messege: if not founded
    """
    if request.method == "GET":
        #list to store the resultDetails. for each post request we have to define new empty resultDetails-list
        resultDetails = []
        # save the json file in jsonDataFromBody
        #jsonDataFromBody contains the informatoins of the photos, that we want query
        jsonDataFromBody = request.json

        try:
            cur = openCursor(mysql)
            #retrive the keys in json file (keys = PhotoNames)
            for key in jsonDataFromBody.keys():
                #select PhotoUrl for each key
                PhotoNameFromDb = selectFromDb(key, cur)
                resultDetails.append(cleanString(PhotoNameFromDb))
        except Exception  as e:
            print(e)
        finally:
            closeConnection(cur)
            #at the we will send back a json file if any result found
            if resultDetails.__len__() > 0:
                return json.dumps(resultDetails)
            #if not send back "No Results Found"
            else:
                return "No Results Found"
    
@app.route("/delete/<int:id>", methods=["DELETE"])
def delete(id):
    """
    delelte Emlement/s from the db
    params:
    id:int for the phtot Id to delet it 
    if id == 0 the all rows in the db will be deleted 
    """
    try:
        cur = openCursor(mysql)
        if deleteFromDb(id, cur) == "all":
            return "All Photos are deleted"
        else:
            return f"row of PhotoId {id} is Deleted"
    except Exception as e:
        print(e)
    finally:
        save(mysql)
        closeConnection(cur)

@app.errorhandler(404)
def page_not_found(error):
    return "Page not found", 404


if __name__ == "__main__":
    app.run(debug=True)
