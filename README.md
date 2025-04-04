# Traffic Sign Recognition

This is a Visual Studio project developed in C#, focused on image processing techniques for the detection and classification of traffic signs (mainly signs with red contours).

The goal of this project was to develop a traffic sign recognition system using classic image processing methods — no machine learning involved. Additionally, this project also functions as a simple GUI application, allowing users to load images, process them, and visualize the results.


## Methodology
This is a high-level overview of the methodology used in this project:

### 1. Image Preprocessing
1. Binarize the image on the red color in the HSV color space.
2. Fill holes and remove noise using morphological operations (close, followed by open, both with a kernel of size 3x3).

### 2. Shape Detection
1. Use connected component labeling to obtain the objects in the image.
2. Process each component:
   - Discard small components based on area.
   - Calculate circularity of each component to determine if the object is circular.
   - If the object is not considered circular, it is classified as a danger sign. If not, further processing is needed to determine if it is a speed sign (and its speed limit) or a prohibition sign.

### 3. Character Extraction
1. To determine the content of a circular sign, connected component labeling is used again on just the circular object after it is preprocessed. Preprocessing includes:
   - Red channel
   - Invert the image
   - Binarization (manual thresholding with threhold = 190)
   - Remove noise (open operation with a kernel of size 3x3)
2. After the connected component labeling, if the number of components is less than 2 or greater than 3, the sign is classified as a prohibition sign. Otherwise, it is considered a speed sign and character recognition is needed to identify the speed limit.

### 4. Character Recognition
1. Reference images of digits (black on white background) are inverted and binarized using Otsu's method to ensure white digits on a black background.
2. The binary image segment corresponding to a potential digit is isolated for comparison and the digit is compared with each template based on pixel similarity. The one with the highest match is selected. To be accepted as a valid speed sign:
   - All digit matches must have ≥ 70% similarity.
   - At least one digit must be a 0.
3.  If the rules are met, the sign is classified as a speed limit sign, and the speed is extracted from the recognized digits.



## Potential Improvements
The suggested improvements are directly related to keeping this project in line with the original goal of using classic image processing methods.

- **Improving Preprocessing for Character Recognition**: Relying on a fixed number of components for character recognition can cause errors. A better approach might be to filter out components smaller than a certain area, avoiding false positives without needing additional rules. Preprocessing before connected component analysis could also be refined to better isolate objects within the sign.

- **More Robust Danger Sign Detection**: A possible solution found in research involves detecting contours, applying approxPolyDP (from OpenCV) to simplify shapes, and counting their edges. However, this method may be overly optimistic, especially when signs are tilted or seen from a perspective that distorts the triangular shape.

### Side Note
The current implementation is limited to signs with red contours. Future work could include extending the system to recognize other colors (e.g., blue, yellow) and shapes (e.g., squares, stop signs). This would, however, be a significant change in the project scope and would require a more complex approach to image processing and classification.


## About
This project was developed as part of the Sensorial Systems (2024/2025) course at FCT-UNL.