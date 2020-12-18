from flask_mysqldb import MySQL
from flask import Flask
from mock import patch
import db
import unittest
import yaml
import dbAPI
import mysql.connector
from mysql.connector import errorcode



mock_app = Flask(__name__)



class MockDB(unittest.TestCase)
    
    @classmethod
    def setUpClass(cls):
        mock_db = yaml.load(open("db.yaml")) 
        
        cnx = mysql.connector.connect(
            host=mock_db['mysql_host'],
            user=mock_db['mysql_user'],
            password=mock_db['mysql_password'],
            port = 3306
        )
        cursor = cnx.cursor(dictionary=True)

        

        testconfig ={
            'host': mock_db['mysql_host'],
            'user': mock_db['mysql_user'],
            'password': mock_db['mysql_password'],
            'database': mock_db['mysql_db']
        }
        cls.mock_db_config = patch.dict(utils.config, testconfig)
      


