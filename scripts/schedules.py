import mysql.connector


mydb_conn = mysql.connector.connect(
    host="qrscan.cjyyynhgqpnb.us-east-2.rds.amazonaws.com",
    user="admin",
    password="ProiectMPS1",
    database="qrscan",
)

mycursor = mydb_conn.cursor()

mydb_conn.close()