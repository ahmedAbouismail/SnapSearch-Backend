
import db

# from unittest.mock import patch
from flask_mysqldb import MySQL
from flask import Flask
# from mock import patch
import unittest
import yaml
from unittest.mock import patch
import mysql.connector



mock_app = Flask(__name__)

class TestDB(unittest.TestCase):
    def setUp(self):
        mock_db = yaml.load(open("db.yaml"))
        mock_app.config['MYSQL_HOST'] = mock_db['mysql_host']
        mock_app.config['MYSQL_USER'] = mock_db['mysql_user']
        mock_app.config['MYSQL_PASSWORD'] = mock_db['mysql_password']
        mock_app.config['MYSQL_DB'] = mock_db['mysql_db']

        mock_mysql = MySQL(mock_app)
        print(mock_mysql)
        with mock_app.app_context():
            cur = mock_mysql.connection.cursor()

        self.mock_connection = patch.object(
            db, 'cur', set_value= cur
        )
        

    def test_configureDbServer(self):
        with self.mock_connection:
            self.assertIsNotNone(db.configureDbServer(mock_app))

    def test_connectMySQLServer(self):
        try:
            with self.mock_connection:
                result = db.connectMySQLServer(mock_app)
                self.assertIsNotNone(result)
        except Exception  as e:
            print(e)
        

    # def test_selectFromDb(self):
    #     print(")))))))))))))))))))))))))))))))))))")
        
    #     with self.mock_connection:
    #         # print(cur)
    #         self.assertIsNotNone(db.selectFromDb("flower"))


if __name__ == '__main__':
    unittest.main()
