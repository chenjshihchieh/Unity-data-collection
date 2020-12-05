import json
import os
from skimage import io, draw

with open('annotation.json', 'r') as json_file_open:
    json_string = json_file_open.read()
    annotation = json.loads(json_string + "]")

image_file_names = os.listdir('images')
for z in range(len(annotation)):

    keypoint_list = annotation[z]['keypoints2d']
    del keypoint_list[2::3]

    capture = io.imread('images/' + image_file_names[z])

    for i in range(0, len(keypoint_list), 2):
        x = keypoint_list[i]
        y = 665 - keypoint_list[i + 1]
        rr, cc = draw.disk((y, x), 3)
        capture[rr, cc] = 255
        
    io.imsave('out/out%d.png' % z, capture)