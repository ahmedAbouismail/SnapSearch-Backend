from image_embeddings.inference import write_tfrecord
from image_embeddings.inference import run_inference
from image_embeddings.knn import get_results
from flask import Flask, request, jsonify
from PIL import Image 
import os
import io



app = Flask(__name__)

@app.route('/', methods=['GET'])
def test():
    return 'This try should work'

@app.route('/uploadimage/<int:result_num>', methods=['POST'])
def process_image(result_num):
    file = request.files['input_img']
    img = Image.open(file.stream)
    current_dir = os.path.dirname(__file__)
    target = f'{current_dir}/system_files/tf_input_image'
    img.save('search.jpeg')
    
    if not os.path.exists(target):
        os.makedirs(target)

    os.replace(f'{current_dir}/search.jpeg', f'{target}/search.jpeg')
    write_tfrecord()
    run_inference()
    result = get_results(k=result_num)
    return result

@app.route('/createrecords', methods=['GET'])
def create_records():
    write_tfrecord (single_photo=False)
    run_inference(single_photo=False)
    return 'Records were successfully created'


if __name__ == "__main__":
    app.run(debug=True, host="0.0.0.0" ,port=80) 