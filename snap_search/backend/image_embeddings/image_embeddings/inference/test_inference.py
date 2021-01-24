import unittest
import inference
import tensorflow as tf
import numpy as np
from unittest.mock import patch
from efficientnet.tfkeras import EfficientNetB0
from pathlib import Path
import pyarrow.parquet as pq
import os



"""
***important:

    1- make sure that you changed the paths to the paths where the files are saved on ur computer.
    2- run the test twice to get the right result.
        a- the first time to create the tf_output file and write the data.
        b- the seconde time to read the data from tf_output and write it in the embeddings_output.
"""
currentDir = os.path.dirname(__file__)
tf_output = os.path.abspath(os.path.join(currentDir, "../../system_files/tf_flower_tf_records"))
embeddings_output = os.path.abspath(os.path.join(currentDir, "../../system_files/tf_flower_embeddings"))
database_folder = os.path.abspath(os.path.join(currentDir, "../../system_files/tf_flower_images"))



class Test_Inference(unittest.TestCase):
    # @classmethod
    
    def test_run_inference(self):
        inference.run_inference(tfrecords_folder=tf_output, output_folder=embeddings_output, batch_size=1000, single_photo=False)

    def test_int64_feature(self):
        """
        pass an bool value to check function
        part_1: self.assertIsNotNone(result) ->
            check if the returned result is not none 
        part_2: self.assertEqual(result, checkValue) ->
            compare the returned resault with fake mock(checkValue)
        """
        value = True
        checkValue = tf.train.Feature(int64_list=tf.train.Int64List(value = [value]))
        result = inference._int64_feature(value)
        print("**********************test_int64_feature************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        self.assertEqual(result, checkValue)
        
    def test_bytes_feature(self):
        """
        test the function with two types String and Byte
        part_1: self.assertIsNotNone(result) ->
            check the returned result is not none 
        part_2: self.assertEqual(result, checkValue) ->
            compare the returned resault with fake mock(checkValueByte)
        """
        valueString = "string"
        checkValueString = tf.train.Feature(bytes_list=tf.train.BytesList(value=[bytes(valueString, 'utf-8')]))
        resultString = inference._bytes_feature(bytes(valueString, 'utf-8'))
        print("**********************test_bytes_feature************************")
        print(resultString)
        print("****************************************************************")
        self.assertIsNotNone(resultString)
        self.assertEqual(resultString, checkValueString)
        

        valuebyte = 16
        checkValueByte = tf.train.Feature(bytes_list=tf.train.BytesList(value=[bytes(valuebyte)]))
        resultByte= inference._bytes_feature(bytes(valuebyte))
        print("**********************test_bytes_feature************************")
        print(resultByte)
        print("****************************************************************")
        self.assertIsNotNone(resultByte)
        self.assertEqual(resultByte, checkValueByte)
        
    def test_serialize_example(self):
        """
        pass fake image and image name to test the function
        part_1: self.assertIsNotNone(result) ->
            check the returned result is not none
        """
        # value = bytes(5)
        image = bytes(9)
         #tf.train.Feature(bytes_list=tf.train.BytesList(value=[bytes(value)]))
        imageName = bytes("image_daisy_45", 'utf-8')
        result  = inference.serialize_example(image, imageName)
        print("**********************test_serialize_example************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)

    def test_tf_serialize_example(self):
        """
        pass fake image and image name to test the function
        part_1: self.assertIsNotNone(result) ->
            check the returned result is not none
        part_2: self.assertEqual(result, tf.reshape(tfString, ())) ->
             compare the returned resault with fake mock(tfString)
        """
        image = bytes(1)
        imageName = bytes("imageName", 'utf-8')
        result = inference.tf_serialize_example(image, imageName)
        print("**********************test_tf_serialize_example************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        
        tfString = tf.py_function(inference.serialize_example, (image, imageName), tf.string)
        self.assertEqual(result, tf.reshape(tfString, ()))

    def test_process_path(self):
        """
        pass a path of image to test the function
        part_1: self.assertIsNotNone(result) ->
            check every part of the result is not None

        part_2: self.assertIsInstance(result[], tf.Tensor) ->
            check every part of the result is an Instance of Tensor

        ***please-> put a path of one photo.jepeg as the example below  
        """
        #path : where the images on my pc located 
        path = "D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_images/image_daisy_45.jpeg"
        result = inference.process_path(path)
        print("**********************test_process_path************************")
        print(result[0].dtype)
        print("****************************************************************")
        #test ervry element in tuple not None
        self.assertIsNotNone(result[0])
        self.assertIsNotNone(result[1])
        self.assertIsInstance(result[0], tf.Tensor)
        self.assertIsInstance(result[1], tf.Tensor)
   
    def test_parse_function(self):
        """
        path .tfrecord to test the func
        part_1: self.assertIsNotNone(result) ->
                check every part of the result is not None
        part_2: self.assertIsInstance(result[], tf.Tensor) ->
            check every part of the result is an Instance of Dataset

        ***please-> put a path of one .tfrecords as the example below
        """
        filename = "D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_tf_records/part-001-db.tfrecord"
        filenames = [filename]
        raw_dataset = tf.data.TFRecordDataset(filenames)
        result = raw_dataset.map(inference._parse_function)
        print("**********************test_parse_function************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        self.assertIsInstance(result, tf.data.Dataset)

    def test_preprocess_image(self):
        """
        path .tfrecord to test the func
        part_1: self.assertIsNotNone(result) ->
                check every part of the result is not None
        part_2: self.assertIsInstance(result[], tf.Tensor) ->
            check every part of the result is an Instance of Dataset

        ***please-> put a path of one .tfrecords as the example below
        """
        #creat fake example_proto for the func
        filename = "D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_tf_records/part-001-db.tfrecord"
        filenames = [filename]
        raw_dataset = tf.data.TFRecordDataset(filenames)
        result = raw_dataset.map(inference._parse_function).map(inference.preprocess_image)
        print("**********************test_preprocess_image************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        self.assertIsInstance(result, tf.data.Dataset)
        
    def test_read_tfrecord(self):
        """
        path .tfrecord to test the func
        part_1: self.assertIsNotNone(result) ->
                check every part of the result is not None
        part_2: self.assertIsInstance(result, tf.Tensor) ->
            check every part of the result is an Instance of Dataset
            self.assertIsInstance(result, tf.data.Dataset)

        ***please-> put a path of one .tfrecords as the example below
        """

        filename = "D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_tf_records/part-001-db.tfrecord"
        result = inference.read_tfrecord(filename)
        print("**********************test_read_tfrecord************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        self.assertIsInstance(result, tf.data.Dataset)
        
    def test_tfrecords_to_write_embeddings(self):
        """
        pass the tfrecords to write embeddings

        part_1: assert os.path.exists(path ->
                check if the embeddings_files has been written 
        """
        model = EfficientNetB0(weights="imagenet", include_top=False, pooling="avg")
        result = inference.tfrecords_to_write_embeddings(tf_output, embeddings_output, model,batch_size=1000, single_photo=False)
        print("**********************test_tfrecords_to_write_embeddings************************")
        print(result)
        print("****************************************************************")
        for i in range(0,10):
            path = ("D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_embeddings/part-00%s-db.parquet" %i)
            assert os.path.exists(path)
        
    def test_list_files(self):
        """
        path the database_folder to test the func

        part_1: self.assertIsNotNone(result) ->
            check every part of the result is not None

        part_2: self.assertIsInstance(result, tf.Tensor) ->
            check every part of the result is an Instance of Dataset
            

        """
        result = inference.list_files(database_folder)
        print("**********************test_list_files************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        self.assertIsInstance(result, tf.data.Dataset)

    def test_read_data_from_files(self):
        """
        test one of the shard_lists

        part_1: self.assertIsNotNone(result) ->
            check every part of the result is not None

        part_2: self.assertIsInstance(result, tf.Tensor) ->
            check every part of the result is an Instance of Dataset
        """
        list_ds = inference.list_files(database_folder)
        shard_list = list_ds.shard(10, 3)
        
        result = inference.read_data_from_files(shard_list)
        print("**********************test_read_data_from_files************************")
        print(result)
        print("****************************************************************")
        self.assertIsNotNone(result)
        self.assertIsInstance(result, tf.data.Dataset)

        
    def test_write_tfrecord(self):
        inference.write_tfrecord(output_folder=tf_output, single_photo=False)
        
        assert os.path.exists(tf_output)


if __name__ == '__main__':
    suite=unittest.TestSuite()
    suite.addTest(Test_Inference("test_run_inference"))
    suite.addTest(Test_Inference("test_int64_feature"))
    suite.addTest(Test_Inference("test_bytes_feature"))
    suite.addTest(Test_Inference("test_serialize_example"))
    suite.addTest(Test_Inference("test_tf_serialize_example"))
    suite.addTest(Test_Inference("test_process_path"))
    suite.addTest(Test_Inference("test_parse_function"))
    suite.addTest(Test_Inference("test_preprocess_image"))
    suite.addTest(Test_Inference("test_read_tfrecord"))
    suite.addTest(Test_Inference("test_tfrecords_to_write_embeddings"))
    suite.addTest(Test_Inference("test_list_files"))
    suite.addTest(Test_Inference("test_read_data_from_files"))
    result = unittest.TextTestRunner(verbosity=2).run(suite)
    if result.wasSuccessful():
        exit(0)
    else:
        exit(1)