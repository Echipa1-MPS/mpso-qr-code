import requests
import time

body = {
    "email": "eliza.apostol@stud.acs.upb.ro",
    "password": "parolaAiaputernic4",
    "role": 2,
    "name": "Eliza",
    "surname": "Apostol",
    "group": "341C1",
    "username": "eliza.apostol"
}

req = requests.post("http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/authentication/register", json=body)
time.sleep(2)


