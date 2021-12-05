import mysql.connector

mydb_conn = mysql.connector.connect(
    host="qrscan.cjyyynhgqpnb.us-east-2.rds.amazonaws.com",
    user="admin",
    password="ProiectMPS1",
    database="qrscan",
)

mycursor = mydb_conn.cursor()

mycursor.execute("SELECT us.user_id FROM user_subject us WHERE us.subject_id = 36 and us.user_id <= 108")

students = mycursor.fetchall()

students_list = list(map(lambda s: s[0], students))


full_strikes = students_list[4:]
print(len(students_list))


sql = "INSERT INTO presence (user_id, qr_code_id) VALUES (%s, %s)"
val = [(students_list[i], 95) for i in range(len(full_strikes))]

mycursor.executemany(sql, val)

mydb_conn.commit()
print(mycursor.rowcount, "was inserted.")

mydb_conn.close()