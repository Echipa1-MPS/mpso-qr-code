import mysql.connector

mydb_conn = mysql.connector.connect(
    host="qrscan.cjyyynhgqpnb.us-east-2.rds.amazonaws.com",
    user="admin",
    password="ProiectMPS1",
    database="qrscan",
)


mycursor = mydb_conn.cursor()
sql = "INSERT INTO subjects (information, name, grading, key_qr_id) VALUES (%s, %s, %s, %s)"
val = [
    (
        "Management-ul proiectelor software este o clasă care vă va învăța cum să planificați și să gestionați proiecte software, să vă alegeți stilul de management adecvat, să utilizați instrumente specifice pentru urmărirea și monitorizarea proiectelor.",
        "MPS",
        "50p parcurs\n50p examen",
        28,
    ),
    (
        "La Introducerea sistemelor informatice vom invata cum sa facem un sistem informatic complex folosind API-uri precum ArcGIS pentru generarea hartilor.",
        "ISI",
        "50p parcurs\n50p examen",
        29,
    ),
    (
        "Vom studia Sisteme multiprocesoarelor facand profiling la mai multe probleme de paralelizare de pe sistem de operare diferite.",
        "SM",
        "50p parcurs\n50p examen",
        30,
    ),
    (
        "Sisteme incorporate de tip embedded in Raspberry Pi.",
        "SI",
        "50p parcurs\n50p examen",
        31,
    ),
    (
        "Baze de date 2 va fi un curs util pentru intelegerea limbajelor NoSQL.",
        "BD2",
        "50p parcurs\n50p examen",
        32,
    ),
]

mycursor.executemany(sql, val)

mydb_conn.commit()
print(mycursor.rowcount, "was inserted.")


mydb_conn.close()
