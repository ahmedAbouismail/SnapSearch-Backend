from flask import Flask, request, redirect, url_for
from flask_mysqldb import MySQL
import yaml
import json
#help contains functions for help
from help import *
app = Flask(__name__)


#open the connection to the MySql
mysql = MySQL(configuration(app))



@app.route("/populate/<int:photoId>/<string:photoName>/<path:photoUrl>", methods=["GET", "POST"])
def populate_db(photoId,photoName,photoUrl):
    if request.method == "POST":

        #to return the status if it's done or not 
        msg = ""

        #Try except bolck to throw any expition
        try:
            #create curser to accese the db
            cur = mysql.connection.cursor()

            #check if the params not empty and the types of the params are correct
            intphotoId= int(photoId)  
            if areParamsCorrect(intphotoId, photoName,photoUrl):
                #insert to the table (result) in db(cbir_result)
                cur.execute("INSERT INTO result(PhotoId, PhotoName, PhotoUrl) VALUES(%s, %s,%s)",(intphotoId, photoName, photoUrl))

                msg = "The data successfuly inserted"
            else:
                msg = "Coudnt't insert the data because of wrong Param"
        except Exception  as e:
            print(e)
        finally:
            #commit the changes in db
            mysql.connection.commit()
            #close the connection
            cur.close()
            return msg
    
      

@app.route("/reults", methods=["GET", "POST"])
def results_db():
    
    if request.method == "GET":
        #list to store the resultDetails. for each post request we have to define new empty resultDetails-list
        resultDetails = []

        # save the json file in jsonDataFromBody
        #jsonDataFromBody contains the informatoins of the photo, that we want query
        jsonDataFromBody = request.json

        #Try except bolck to throw any expition 
        try:
            #make environment to query the Db
            cur = mysql.connection.cursor()

            #retrive the keys in json file (keys = PhotoNames)
            for key in jsonDataFromBody.keys():

                #select each PhotoUrl for each key
                resultValue = cur.execute(f"SELECT PhotoUrl FROM result WHERE PhotoName = '{key}'")

                #if PhotoUrl found? resultValue will be bigger than 0 

                if resultValue > 0:
                    #cur.fetchone() to fetch the result that we found
                    #PhotoNameFromDb is string with a lot special characters
                    PhotoNameFromDb = str(cur.fetchall())

                    #send PhotoNameFromDb to cleanString fnuction to return string without 
                    # special characters and add it to resultDetails
                    resultDetails.append(cleanString(PhotoNameFromDb))
            #To print in prompt       
            # for result in resultDetails:
            #     print(result) 

        #throw the exp if any
        except Exception  as e:
            print(e)

        
        finally:
            #close the connection
            cur.close()

            #at the we will send back a json file if any result found
            if resultDetails.__len__() > 0:
                return json.dumps(resultDetails)

            #if not send back "No Results Found"
            else:
                return "No Results Found"
    
        
@app.errorhandler(404)
def page_not_found(error):
    return "Page not found", 404

if __name__ == "__main__":
    app.run(debug=True)
