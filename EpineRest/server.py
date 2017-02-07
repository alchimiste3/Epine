# -*- coding: utf-8 -*-

from flask import Flask, render_template
from flask import request
from flask import g

app = Flask(__name__)

info = ""
meteo = ""
lum = ""
pres = ""
humTerre = ""
humAir = ""
temp = ""
dist = ""

@app.route('/')
def hello_world():
    global info
    global meteo
    global lum
    global pres
    global humTerre
    global humAir
    global temp
    global dist
    return render_template('index.html', lum = lum, pres=pres, humTerre = humTerre, humAir = humAir, temp=temp, dist=dist, info=info, meteo=meteo)


@app.route('/cxf/ocs/interface', methods=['GET', 'POST'])
def upload_data():
    global info
    global meteo
    global lum
    global pres
    global humTerre
    global humAir
    global temp
    global dist
    if request.method == 'GET':
        return render_template('index.html', lum = lum, pres=pres, humTerre = humTerre, humAir = humAir, temp=temp, dist=dist, info=info, meteo=meteo)

    if request.method == 'POST':
        print(lum)
        print("Receiving post")
        print request.data
        print request.mimetype
        print request.json
        json = request.get_json(force=True)
        lum = json["lum"]
        pres = json["pres"]
        humTerre = json["humTerre"]
        humAir = json["humAir"]
        temp = json["temp"]
        dist = json["dist"]
        info = json["info"]
        meteo = json["meteo"]
    print("Data received")
    json = request.get_json()
    print_all()
    return 'OK'

def print_all():
    print("Luminosité: " + str(lum))
    print("Pression: " + str(pres))
    print("HumTerre: " + str(humTerre))
    print("HumAir: " + str(humAir))
    print("Temperature: " + str(temp))
    print("Distance: " + str(dist))

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
