import yaml

#to set the config for the app and return back the configuierd app  
def configuration(app):
    #open and load db.yaml in db
    db = yaml.load(open("db.yaml"))
    #db["key"] to get the value
    app.config['MYSQL_HOST'] = db['mysql_host']
    app.config['MYSQL_USER'] = db['mysql_user']
    app.config['MYSQL_PASSWORD'] = db['mysql_password']
    app.config['MYSQL_DB'] = db['mysql_db']
    return app

#check if there is any mistak in the Parmas to the Url
def areParamsCorrect(photoId ,photoName,photoUrl):
    # chek if any param = None
    if photoId != None and photoName != None and photoUrl != None:
            #return true if all params are't None 
            return True   
    else:
        print("One or more of the Params = None")
        return False

def cleanString(text):
    pureString = ""
    for char in text:
        #we want ot keep / for the Url
        if(char != '/'):
            if char.isalnum():
                pureString += char
        else:
            pureString += char
    return pureString