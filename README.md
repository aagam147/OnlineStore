# OnlineStore

#Docker
- docker compose -f ./docker-compose.yml up --build

  
- docker swarm init
- docker stack deploy -c compose.yml onlinestore
  - docker service scale onlinestore_onlinestoremvc=3
- docker swarm leave --force

# How to use  
- Run on - http://localhost:5000/
- Admin Credentials
  - UserName: admin
  - Password: Admin
