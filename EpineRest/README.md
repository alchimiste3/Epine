Create a virtualenv (venv need to be installed)
```bash
virtualenv venv
```

Begin using virtual environment (only for development):
```bash
source venv/bin/activate
```
Restore the venv using
```bash
pip install -r requirements.txt
```

If Flask is not found, install it using 
```bash
[sudo] pip install flask
```

To simply launch the application:
```bash
python server.py
```
Open a web browser and go to localhost:5000