kubectl apply -f empldb-secret-config.yaml && sleep 2 && kubectl get secrets,configmap
kubectl apply -f empldb-pv-pvc.yaml && sleep 5 && kubectl get pv,pvc
kubectl apply -f empldb-ssets-svc.yaml && sleep 10 && kubectl get svc
kubectl get pods -l app=mysql
kubectl exec empldb-ssets-0 -- sh -c 'MYSQL_PWD=$MYSQL_ROOT_PASSWORD mysql -u root --table -e "SELECT * FROM Employees;" EmployeeDb'
kubectl apply -f empldb-seed-job.yaml && sleep 15 && kubectl get jobs
kubectl exec empldb-ssets-0 -- sh -c 'MYSQL_PWD=$MYSQL_ROOT_PASSWORD mysql -u root --table -e "SELECT * FROM Employees;" EmployeeDb'
kubectl apply -f emplapi-depl-svc.yaml && sleep 10 && kubectl get svc
kubectl get pods -l app=employeeapi
kubectl apply -f emplapi-ing.yaml && sleep 5 && kubectl get ingress
kubectl apply -f emplapi-hpa.yaml && sleep 2 && kubectl get hpa
kubectl get all