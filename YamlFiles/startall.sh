kubectl apply -f empldb-secret-config.yaml
kubectl apply -f empldb-pv-pvc.yaml
kubectl apply -f empldb-ssets-svc.yaml
kubectl apply -f emplapi-depl-svc.yaml
kubectl apply -f emplapi-ing.yaml
kubectl apply -f emplapi-hpa.yaml