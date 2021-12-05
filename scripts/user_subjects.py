import mysql.connector
from random import shuffle

mydb_conn = mysql.connector.connect(
    host="qrscan.cjyyynhgqpnb.us-east-2.rds.amazonaws.com",
    user="admin",
    password="ProiectMPS1",
    database="qrscan",
)

mycursor = mydb_conn.cursor()

student_ids = [student_id for student_id in range(69, 109)]
teacher_ids = [teacher_id for teacher_id in range(110, 115)]
user_ids = student_ids + teacher_ids
subject_ids = [subject_id for subject_id in range(32, 37)] * 100


shuffle(user_ids)
shuffle(subject_ids)
sql = "INSERT INTO user_subject (user_id, subject_id) VALUES (%s, %s)"
val = [(user_ids[i], subject_ids[i]) for i in range(len(user_ids))]

mycursor.executemany(sql, val)

mydb_conn.commit()
print(mycursor.rowcount, "was inserted.")


mydb_conn.close()
