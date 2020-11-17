from image_embeddings.inference import write_tfrecord
from image_embeddings.inference import run_inference

write_tfrecord ('tf_flower_images', 'tf_flower_tf_records')
run_inference('tf_flower_tf_records', 'tf_flower_embeddings')