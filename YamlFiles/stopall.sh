kubectl delete -f emplapi-hpa.yaml
kubectl delete -f emplapi-depl-ing.yaml
kubectl delete -f empldb-hl-ssets.yaml
kubectl delete -f empldb-pv-pvc.yaml
kubectl delete -f empldb-secret-config.yaml
rm *.*