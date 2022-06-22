# conint_project

## backend für semesterprojekt
- Im Ordner data ist csv datei abgelegt "BTC-USD"
- mit postman auf http://localhost:1300/server/ einen request schicken (muss GET sein)
- als antwort kommt ein JSON kommen mit dem Inhalt der CSV Datei

## dockerfile
- docker build -t conint.backend .
- Docker run  --rm -p  1300:1300 mhaleesi/conint.backend:latest


