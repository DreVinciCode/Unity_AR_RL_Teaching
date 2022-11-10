# Unity_AR_RL_Teaching


###
Notes:
Using python 3.9.9
Navigate to Unity project directory

Create virtual environment
$ python -m venv venv

Activate environment
$ venv\Scripts\activate

Update pip
$ python -m pip install --upgrade pip

Install PyTorch
$ pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html

Install mlagents
$ python -m pip install mlagents==0.28.0

Check if installation works
$ mlagents-learn --help

To train the agent
$ mlagents-learn --force
or 
$ mlagents-learn --run-id=<name>

To train with config(yaml) file
$ mlagents-learn <pathToYamlFile/BehaviorName.yaml> --run-id=<filename>

To show tensorboard results (most likely shown on localhost)
$ tensorboard --logdir results