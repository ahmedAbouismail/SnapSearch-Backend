
def areParamsCorrect(photoId ,photoName,photoUrl):
    """
    check if there is any mistak in the Parmas of the populate route
    return->
    true: if all params not equal None
    flase: if any param equal None 
    """
    if photoId != None and photoName != None and photoUrl != None:
        return True   
    else:
        print("One or more of the Params equal None")
        return False

def cleanString(text):
    """
    to make sure that the strnig hasn't any special characters and return it back
    """
    pureString = ""
    for char in text:
        #we want ot keep / for the Url
        if(char != '/'):
            if char.isalnum():
                pureString += char
        else:
            pureString += char
    return pureString
