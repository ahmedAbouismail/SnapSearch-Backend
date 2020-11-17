from image_embeddings.inference import write_tfrecord
from image_embeddings.inference import run_inference
from image_embeddings.knn import get_results
from flask import Flask, request, jsonify
from PIL import Image 
import os
import io

app = Flask(__name__)

@app.route('/uploadimage/<int:result_num>', methods=['GET','POST'])
def process_image(result_num):
    file = request.files['input_img']
    img = Image.open(file.stream)
    currentDir = os.path.dirname(__file__)
    img.save('search.jpeg')
    os.replace(f'{currentDir}/search.jpeg', f'{currentDir}/tf_input_image/search.jpeg')
    write_tfrecord ('tf_input_image', 'tf_flower_tf_records')
    run_inference('tf_flower_tf_records', 'tf_flower_embeddings')
    result = get_results('tf_flower_embeddings', result_num)
    return result



if __name__ == "_main_":
    app.run(debug=True)

