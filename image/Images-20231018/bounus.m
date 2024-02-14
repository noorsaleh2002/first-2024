A=imread('coins.png');
B=im2bw(A,graythresh(A)-0.15);
c=imopen(B,ones(5,5));
s=size(c);
count=0;
f=ones(3,3);
f(2,2)=0;
g=ordfilt2(c,8,f);
for i=1:15
    g=imerode(g,ones(3,3));
end
imshow(g);
result=zeros(s(1),s(2));
struc=strel('disk',5);
result=imerode(g,struc);
% [x y]=bwlabel(g);
% disp('count: ');
% y

imshow(result);





