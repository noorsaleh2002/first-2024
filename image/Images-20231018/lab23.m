
A=imread('image1.png');
s=size(A);
he=histeq(A);
figure(1),imshow(he);
c=imadjust(A,[],[],0.5);
B=zeros(s(1),s(2));
B(1:s(1)/2,:)=c(1:s(1)/2,:);
c=imadjust(A,[],[],2);
B(s(1)/2+1:end,:)=c(s(1)/2+1:end,:);
figure(2),imshow(B/255);
av=fspecial('average',[5,7]);
B=filter2(av,A);
figure(3),imshow(B/255);

F=[-1 -1 -1 ;-1 9 -1 ; -1 -1 -1];
B=filter2(F,A);
figure(4),imshow(B/255);


