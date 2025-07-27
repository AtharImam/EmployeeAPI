kubectl apply -f empldb-secret-config.yaml
kubectl apply -f empldb-pv-pvc.yaml
kubectl apply -f empldb-hl-ssets.yaml
kubectl apply -f emplapi-depl-ing.yaml
kubectl apply -f emplapi-hpa.yaml