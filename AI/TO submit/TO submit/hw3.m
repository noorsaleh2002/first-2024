%there is other way for imorting the tabel by cliking the file and import it as "MATRIC" 
%but i prefer using funtion "redtable" for quik running 

A=readtable('housedata.csv');
%All the input and output traing samples
trained=A(1:700,:);
%All the inut and ouput validation,testing samples
validation=A(701:end,:);
%converting the table to array for easy dealing over table
x=table2array(trained(:,1:10));
y=table2array(trained(:,11));

in=table2array(validation(:,1:10));
ou=table2array(validation(:,11));
%convetign array "matrix" to vectors
x=x';
y=y';


in=in';
ou=ou';
%creating nural network object RANDOM ONE
Net=feedforwardnet([4 3 2]);
%determine where to stop training for better accutrecy for future
Net.trainParam.goal=0.001;
Net.trainParam.epochs=1000;
%Entreing all the (700) sample for traning
Net.DivideFCN="";

net1=train(Net,x,y);
%test the validation samples

test=net1(in);
%sum all the ones (miss oututs) in vector test
missnum=sum(abs(round(ou-test)));
%see the accurecy for the network 
%اقل من تسعين بس اقصى شي وصلت كان الرقم 89.5

disp('Accuracy : ');
accurecy=((length(in)-missnum)/length(in))*100


