A=imread('coins.png');
h=imhist(A);
s=size(A);
figure(1);
plot(h);
B=zeros(s(1),s(2));
for i=1:s(1)
  for j=1:s(2)
    if A(i,j)<50
      B(i,j)=A(i,j)*2;
    elseif A(i,j)>50 || A(i,j)<150
      B(i,j)=A(i,j)+100;
    else
      B(i,j)=A(i,j);
    endif
  endfor


endfor
figure(2);
imshow(B/255);
A=255-A;
figure(3);
imshow(A);
A7=mod(floor(A/128),2);
figure(6);
imshow(double(A7));


