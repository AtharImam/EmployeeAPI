kubectl delete -f emplapi-hpa.yaml
kubectl delete -f emplapi-ing.yaml
kubectl delete -f emplapi-depl-svc.yaml
kubectl delete -f empldb-seed-job.yaml
kubectl delete -f empldb-ssets-svc.yaml
kubectl delete -f empldb-pv-pvc.yaml
kubectl delete -f empldb-secret-config.yaml
rm *.*