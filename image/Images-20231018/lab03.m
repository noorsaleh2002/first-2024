A=imread('image1.png');
ha=histeq(A);
figure(1),imshow(ha);
s=size(A);
B=zeros(s(1),s(2));
c=imadjust(A,[],[],0.5);
B((1:s(1)/2),:)=c((1:s(1)/2),:);
c=imadjust(A,[],[],2);
B((s(1)/2+1):end,:)=c(  (s(1)/2+1):end,:);
figure(2),imshow(B/255);
hb=imhist(B);
figure(3),plot(hb);
av=fspecial('average',[5,7]);
aav=filter2(av,A);
figure(4),imshow(aav/255);
mask=[-1 -1 -1;-1 9 -1; -1 -1 -1];
amask=filter2(mask,A);
figure(5),imshow(amask/255);


