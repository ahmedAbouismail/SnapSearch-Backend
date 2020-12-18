import unittest
import inference
import tensorflow as tf
import numpy as np
from unittest.mock import patch
from efficientnet.tfkeras import EfficientNetB0


class TeastInference(unittest.TestCase):
    # @classmethod

    database_folder = 'system_files/tf_flower_images'
    input_image_folder = 'system_files/tf_input_image'
    tf_output = 'system_files/tf_flower_tf_records'
    embeddings_output = 'system_files/tf_flower_embeddings'

    def test_int64_feature(self):
        value = True
        checkValue = tf.train.Feature(int64_list=tf.train.Int64List(value = [value]))
        result = inference._int64_feature(value)
        self.assertEqual(result, checkValue)
        self.assertIsNotNone(result)

    def test_bytes_feature(self):
        valueString = "string"
        checkValueString = tf.train.Feature(bytes_list=tf.train.BytesList(value=[bytes(valueString, 'utf-8')]))
        resultString = inference._bytes_feature(bytes(valueString, 'utf-8'))
        self.assertEqual(resultString, checkValueString)
        self.assertIsNotNone(resultString)

        valuebyte = 16
        checkValueByte = tf.train.Feature(bytes_list=tf.train.BytesList(value=[bytes(valuebyte)]))
        resultByte= inference._bytes_feature(bytes(valuebyte))
        self.assertEqual(resultByte, checkValueByte)
        self.assertIsNotNone(resultByte)

    def test_serialize_example(self):
        value = bytes(12)
        image = bytes(1) #tf.train.Feature(bytes_list=tf.train.BytesList(value=[bytes(value)]))
        imageName = bytes("imageName", 'utf-8')
        result  = inference.serialize_example(image, imageName)
        self.assertIsNotNone(result)

    def test_tf_serialize_example(self):
        image = bytes(1)
        imageName = bytes("imageName", 'utf-8')
        result = inference.tf_serialize_example(image, imageName)
        self.assertIsNotNone(result)
        #test with fake tf.reshape
        tfString = tf.py_function(inference.serialize_example, (image, imageName), tf.string)
        self.assertEqual(result, tf.reshape(tfString, ()))

    def test_process_path(self):
        #path : where the images on my pc located 
        path = "D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_images/image_daisy_45.jpeg"
        result = inference.process_path(path)
        #test ervry element in tuple not None
        self.assertIsNotNone(result[0])
        self.assertIsNotNone(result[1])

    def test_parse_function(self):
       
        #creat fake example_proto for the func
        filename = "file"
        filenames = [filename]
        raw_dataset = tf.data.TFRecordDataset(filenames)
        result = raw_dataset.map(inference._parse_function)
        self.assertIsNotNone(result)

    def test_preprocess_image(self):
        #creat fake example_proto for the func
        filename = "file"
        filenames = [filename]
        raw_dataset = tf.data.TFRecordDataset(filenames)
        result = raw_dataset.map(inference._parse_function).map(inference.preprocess_image)
        self.assertIsNotNone(result)
        
    def test_read_tfrecord(self):
        filename = "file"
        result = inference.read_tfrecord(filename)
        self.assertIsNotNone(result)
        
    @patch('builtins.print')
    def test_tfrecords_to_write_embeddings(self):
        model = EfficientNetB0(weights="imagenet", include_top=False, pooling="avg")
        inference.tfrecords_to_write_embeddings(tf_output, embeddings_output, model,batch_size=1000, single_photo=True)
    def test_list_files(self):
        imagePath = "D:/HTW/Software Entwicklung/CBIR DB/gl/team9/snap_search/backend/image_embeddings/system_files/tf_flower_images"
        result = inference.list_files(imagePath)
        print(result)
        self.assertIsNotNone(result)


    def test_read_data_from_files()

if __name__ == '__main__':
    unittest.main()