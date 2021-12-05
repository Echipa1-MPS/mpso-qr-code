import mysql.connector
import time
import random

import datetime

mydb_conn = mysql.connector.connect(
    host="qrscan.cjyyynhgqpnb.us-east-2.rds.amazonaws.com",
    user="admin",
    password="ProiectMPS1",
    database="qrscan",
)

mycursor = mydb_conn.cursor()

schedule_ids = [31, 32, 33]
reps = 3
sesh_legth = 20
days = [14, 15, 16]
hours = [10, 12, 14, 16]

def format_dates(day: int) -> (str, str):
    start_date = datetime.datetime(2021, 12, day, random.choice(hours))
    formatted_date = start_date.strftime('%Y-%m-%d %H:%M:%S')
    datetime_object = datetime.datetime.strptime(formatted_date, '%Y-%m-%d %H:%M:%S')
    new_datetime = datetime_object + datetime.timedelta(0, reps * sesh_legth)
    formatted_date_finish = new_datetime.strftime('%Y-%m-%d %H:%M:%S')
    print(formatted_date, formatted_date_finish)
    return (formatted_date, formatted_date_finish)

for schedule_id in schedule_ids:
    for day in days:
        for _ in range(3):
            sql = "INSERT INTO qr_code (date, date_finish, schedule_id) VALUES (%s, %s, %s)"
            (date, date_finish) = format_dates(day)
            val = (date, date_finish, schedule_id)

            mycursor.execute(sql, val)

            mydb_conn.commit()
            print(mycursor.rowcount, "was inserted.")
            time.sleep(2)

mydb_conn.close()