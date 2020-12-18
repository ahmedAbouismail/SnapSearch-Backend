from mock_db import *
import unittest
import db
import dbAPI
import mock_db



class TestDB(MockDB):

    def test_configureDbServer(self):
        with self.mock_db_config:
            self.assertIsNotNone(db.configureDbServer(mock_db.mock_app))

    # def test_connectMySQLServer(self):
    #     result = db.connectMySQLServer(dbAPI.app)
    #     self.assertIsNotNone(result)
    
    # def test_openCursor(self):
    #     with self.mock_db_config:
    #         self.assertIsNotNone(db.openCursor(MockDB.mock_mysql))
    #     # result = db.openCursor(dbAPI.mysql)
    #     # self.assertIsNotNone(result)

    # def test_insertInDb(self):
    #     with self.mock_db_config:
    #         self.assertTrue(db.insertInDb(int(1),str('Ahmed'), str('Ahmed'), MockDB.test))
    def test_selectFromDb(self):
        print(")))))))))))))))))))))))))))))))))))")
        print(MockDB.test)
        with self.mock_db_config:
            self.assertIsNotNone(db.selectFromDb("flower", MockDB.test))
        
    
        


if __name__ == '__main__':
    unittest.main()