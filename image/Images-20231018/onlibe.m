A=imread('coins.png');
s=size(A);
h=imhist(A);
figure(1),plot(h);
eq=histeq(A);
figure(2),imshow(eq);
B=zeros(s(1),s(2));
F=imadjust(A,[],[],2.5);
B(1:s(1)/2,:)=F(1:s(1)/2,:);
B(s(1)/2+1:end,:)=A(s(1)/2+1:end,:);
Hb=imhist(B);
figure(3),imshow(B/255);
figure(4),plot(Hb);

avg=fspecial('average',[5,5]);
Z=filter2(avg,A);
figure(5),imshow(Z/255);


