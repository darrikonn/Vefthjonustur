curl -i -X GET localhost:5000/api/companies
curl -i -X GET localhost:5000/api/companies/1
curl -i -X GET localhost:5000/api/users
curl -i -H "Authorization: WubbaLubbaDubDub" -d "name=Te&Kaffi&punchCount=2" -X POST localhost:5000/api/companies
curl -i -H "Authorization: WubbaLubbaDubDub" -d "name=Darri Steinn&gender=m" -X POST localhost:5000/api/users
curl -i -H "Authorization: 1" -d "id=1" -X POST localhost:5000/api/my/punches
