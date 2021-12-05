import requests
import time
import random

from typing import List

names = [
    "Anca",
    "Bianca",
    "Bogdan",
    "Claudiu",
    "Constantin",
    "Cornel",
    "Cosmin",
    "Cristian",
    "Dan",
    "Doru",
    "Dragos",
    "Eliza",
    "Emil",
    "Eugen",
    "Eusebiu",
    "Filip",
    "Flaviu",
    "Florin",
    "Gabriel",
    "George",
    "Grigore",
    "Horia",
    "Ioan",
    "Irina",
    "Lavinia",
    "Larisa",
    "Madalina",
    "Maria",
    "Raluca",
    "Ramona",
    "Roxana",
    "Sabina",
    "Sara",
    "Silviu",
    "Sorin",
    "Stefan",
    "Teodor",
    "Tiberiu",
    "Vasile",
    "Vlad"
]

surnames = [
    "Albescu",
    "Albu",
    "Aldea",
    "Andrei",
    "Baciu",
    "Balan",
    "Barbu",
    "Botezatu",
    "Bucur",
    "Calafeteanu",
    "Cazacu",
    "Ciobanu",
    "Cojocaru",
    "Craioveanu",
    "Creţu",
    "Dalca",
    "Dan",
    "Dascălu",
    "Dragavei",
    "Enache",
    "Fieraru",
    "Floarea",
    "Florescu",
    "Funar",
    "Gheata",
    "Grosu",
    "Hatmanu",
    "Hofer",
    "Iordanescu",
    "Ioveanu",
    "Josan",
    "Lazarescu",
    "Luca",
    "Lungu",
    "Lupu",
    "Marin",
    "Moisil",
    "Nedelcu",
    "Popa",
    "Sandu",
]

groups = ["341C1", "342C2", "343C3", "344C4", "345C5"]

def generate_email(name: str, surname: str) -> str:
    return f"{name.lower()}.{surname.lower()}@stud.acs.upb.ro"

def generate_username(name: str, surname: str) -> str:
    return f"{name.lower()}.{surname.lower()}"

def generate_body() -> List[dict]:
    requests = list()
    for i in range(len(names)):
        request = dict()
        request["email"] = generate_email(name=names[i], surname=surnames[i])
        request["password"] = "qwertyuiop"
        request["role"] = 2
        request["name"] = names[i]
        request["surname"] = surnames[i]
        request["group"] = random.choice(groups)
        request["username"] = generate_username(name=names[i], surname=surnames[i])
        requests.append(request)
    return requests


def add_students() -> None:
    request_bodies = generate_body()
    for body in request_bodies:
        requests.post("http://ec2-3-18-103-144.us-east-2.compute.amazonaws.com:8080/api/user/authentication/register", json=body)
        time.sleep(2)
        print(body["name"] + " is inserted to the database.")
    

add_students()

