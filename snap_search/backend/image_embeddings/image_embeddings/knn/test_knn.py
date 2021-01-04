import unittest
import knn
import os
import sys
currentDir = os.path.dirname(__file__)
sys.path.append(os.path.abspath(os.path.join(currentDir,"../inference")))
import inference


tf_output = os.path.abspath(os.path.join(currentDir,"../../system_files/tf_flower_tf_records"))
embeddings_output = os.path.abspath(os.path.join(currentDir,"../../system_files/tf_flower_embeddings"))
database_folder = os.path.abspath(os.path.join(currentDir,"../../system_files/tf_flower_images"))
input_image = os.path.abspath(os.path.join(currentDir,"../../system_files/tf_input_image"))
index = []


class TestKnn(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        """
        Create the files before the test in order to 
        check the results during the test
        """
        inference.write_tfrecord (single_photo=False)
        inference.run_inference(single_photo=False)
        inference.write_tfrecord()
        inference.run_inference()
    def test_read_embeddings(self):
        results = knn.read_embeddings(embeddings_output)
        index = results
        self.assertIsNotNone(results)
        
    def test_build_index(self):
        [id_to_name, _, embeddings] = knn.read_embeddings(embeddings_output)
        results = knn.build_index(embeddings)
        self.assertIsNotNone(results)
 
    def test_get_results(self):
        results = knn.get_results()
        self.assertIsNotNone(results)

if __name__ == '__main__':
    unittest.main()