import requests
import time
import random

from typing import List

names = [
    "Adelina",
    "Adina",
    "Anton",
    "Cezar",
    "Ciprian"
]

surnames = [
    "Avram",
    "Badea",
    "Coman",
    "Dobre",
    "Ivanovici"
]

def generate_email(name: str, surname: str) -> str:
    return f"{name.lower()}.{surname.lower()}@cs.upb.ro"

def generate_username(name: str, surname: str) -> str:
    return f"{name.lower()}.{surname.lower()}"

def generate_body() -> List[dict]:
    requests = list()
    for i in range(len(names)):
        request = dict()
        request["email"] = generate_email(name=names[i], surname=surnames[i])
        request["password"] = "qwertyuiop"
        request["role"] = 1
        request["name"] = names[i]
        request["surname"] = surnames[i]
        request["group"] = '-'
        request["username"] = generate_username(name=names[i], surname=surnames[i])
        requests.append(request)
    return requests


def add_teachers() -> None:
    request_bodies = generate_body()
    for body in request_bodies:
        requests.post("http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/authentication/register", json=body)
        time.sleep(2)
        print(body["name"] + " is inserted to the database.")
    

add_teachers()