curl -i -X POST -d "name=prump&punchCount=10" localhost:5000/api/companies
curl -i -X POST -d "name=skitur&punchCount=5" localhost:5000/api/companies
curl -i -X POST -d "name=skitur" localhost:5000/api/companies
curl -i -X POST -d "punchCount=5" localhost:5000/api/companies
curl -i -X POST localhost:5000/api/companies
curl -i -X POST -d "name=Darri Steinn Konradsson&email=darrik13@ru.is" localhost:5000/api/users
curl -i -X POST -d "name=dabbeg&email=davidgh13@ru.is" localhost:5000/api/users
curl -i -X POST -d "name=Darri Steinn Konradsson&email=darrik13ru.is" localhost:5000/api/users
curl -i -X POST -d "name=dabbeg" localhost:5000/api/users
curl -i -X POST localhost:5000/api/users
curl -i -X POST -d "email=davidgh13@ru.is" localhost:5000/api/users
curl -i -X POST -d "company=0" localhost:5000/api/users/0/punches
curl -i -X POST -d "company=1" localhost:5000/api/users/0/punches
curl -i -X POST -d "company=2" localhost:5000/api/users/0/punches
curl -i -X POST -d "company=1" localhost:5000/api/users/2/punches
curl -i -X POST localhost:5000/api/users/1/punches
