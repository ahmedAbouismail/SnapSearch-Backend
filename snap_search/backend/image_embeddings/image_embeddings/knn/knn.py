from dataclasses import dataclass
from IPython.display import Image, display
from ipywidgets import widgets, HBox, VBox
from pathlib import Path
import faiss
import numpy as np
import random
import json
import pyarrow.parquet as pq

def read_embeddings(path):
    emb = pq.read_table(path).to_pandas()
    id_to_name = {k: v.decode("utf-8") for k, v in enumerate(list(emb["image_name"]))}
    name_to_id = {v: k for k, v in id_to_name.items()}
    embgood = np.stack(emb["embedding"].to_numpy())
    return [id_to_name, name_to_id, embgood]


def embeddings_to_numpy(input_path, output_path):
    emb = pq.read_table(input_path).to_pandas()

    Path(output_path).mkdir(parents=True, exist_ok=True)
    id_name = [{"id": k, "name": v.decode("utf-8")} for k, v in enumerate(list(emb["image_name"]))]
    json.dump(id_name, open(output_path + "/id_name.json", "w"))

    emb = np.stack(emb["embedding"].to_numpy())
    np.save(open(output_path + "/embedding.npy", "wb"), emb)


def build_index(emb):
    d = emb.shape[1]
    xb = emb
    index = faiss.IndexFlatIP(d)
    index.add(xb)
    return index


def get_results(path, k):
    [id_to_name, name_to_id, embeddings] = read_embeddings(path)
    index = build_index(embeddings)
    for key, name in id_to_name.items():
        if name == "search":
            p = key
    print(id_to_name[p]) 
    res = search(index, id_to_name, embeddings[p], k)
    results = {}
    for score, image_id in res:
        d = {
            str(image_id): {
                'score': "{:.3f}".format(float(score))
            }
        }
        results.update(d)  
    return json.dumps(results, indent=2)  


def search(index, id_to_name, emb, k):
    D, I = index.search(np.expand_dims(emb, 0), k)  # actual search
    return list(zip(D[0], [id_to_name[x] for x in I[0]]))
