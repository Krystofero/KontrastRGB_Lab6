Edytor kontrastu RGB zdjęć napisany w języku C# w środowisku Visual Studio. 
Pozwala na wczytanie dowolnego zdjęcia zmianę kontrasu RGB oraz wyświelenie histogramu RGB.

Aby włączyć program wystarczy otworzyć plik WindowsFormsApplication12.exe z folderu WindowsFormsApplication12/bin/Debug/

Plik: Edytor.Designer.cs tworzy interfejs GUI za pomocą Projektanta formularzy systemu Windows.
Plik: Edytor.cs zawiera kod opisujący funkcje wywoływane dla poszczególnych komponentów składających się na GUI.

W chwili otwarcia programu ukazuje nam się pierwsze okno interfejsu. 
Zawiera ono dwa PictureBox'y (pierwszy to obraz wejściowy a drugi wyjściowy). 
PictureBoxy automatycznie dopasowują swój rozmiar do paneli w których się znajdują po wczytaniu zdjęcia.(Dzięki właściwości SizeMode = AutoSize)
Wczytujemy dowolny plik png. lub jpg.(klikając przycisk wczytaj otwiera się openFileDialog).
Następnie możemy edytować kontrast jego barw wpisując odpowiednie wartość w pole numericUpDown(ma one odpowiednio ograniczone możliwe do wpisania wartości),
a następnie klikając Button. Po wciśnięciu przycisku wyświetla nam się również histogram. Zaznaczając odpowiednie checkBox'y możemy wyświetlać konkretne składowe RGB.
Przycisk zapisz pozwala zapisać plik wynikowy o nazwie wpisanej w textBox (wcześniej należy zmienić ścieżkę docelową na odpowiednią dla naszego urządzenia).

private void button12_Click(object sender, EventArgs e)
    {
        if(pictureBox2.Image != null)
        {
            pictureBox2.Image.Save("C:\\Users\\krzys\\OneDrive\\Obrazy\\JPG\\"+ textBox1.Text + "(zmienione).jpg", ImageFormat.Jpeg);
        }
        
Dwa zaimplementowane w programie algorytmy bazują na Bitmapach zdjęć PictureBox'ów . 
Wczytują wartość koloru czerwonego/zielonego/niebieskiego każdego z pikseli zdjęcia/zdjęć wejściowych, zmieniając w odpowiedni dla danego algorytmu sposób ich wartość i przypisując ją dla pikseli obrazu wyjściowego.
Wykorzystany został wariant nr.1 z wykładu/laboratorium. Jeżeli wpisana w numericUpDown (val) wartość jest większa/równa 0, wówczas stosowany jest wzór:
v’(x,y)=(127/(127-c))*(v(x,y)-c) -> zwiększanie kontrastu
Natomiast gdy jest mniejsza od 0 do -128 wzór:
v’(x,y)=((127+c)/127)*(v(x,y)-c) -> zmniejszenie kontrastu

Na koniec odświeżana jest zawartość wyjściowego pictureBox'a oraz aktualizowany jest histogram(odpowiada za to funkcja show_Histogram).
Każdemu pikselowi przypisana jest intensywność w skali od 0 - całkiem ciemy, do 255 - najaśniejszy.
Zliczana jest liczba pikseli zdjęcia o danej jasności i pokazyana na histogramie.
