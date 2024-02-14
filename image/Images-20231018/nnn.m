%im=input('Enter image name: ');
%A=imread(im);
A = [39 39 126 126;39 39 126 126;39 39 126 126;39 39 126 126];
s=size(A);
vecA=reshape(A',[1 16]);
%imshow(A);
counter=0;
dicMaxNum=2^12-1;
dictionary=containers.Map;
index=256;
x=ones(1,40);
%creating firt dictionary
for i=0:255
  dictionary(num2str(i))=i;
end
currentpic=num2str(vecA(1));
x=2;
f=fopen('ziptext.txt','w');
while x<=length(vecA)+1

  if !isKey(dictionary,currentpic)

    %adding new index
    dictionary(currentpic)=index;

    sym=last;
    fprintf(f,num2str(dictionary(sym)));
    counter=counter+1;
    currentpic=now;
    index=index+1;
    fprintf(f,'   ');


  else

   last=currentpic;
   currentpic=strcat(currentpic,num2str(vecA(x)));
   now=num2str(vecA(x));
     x=x+1;


  endif

end
fclose('ziptext.txt');
%CR=(s(1)*s(2)*8)/(counter*9)
