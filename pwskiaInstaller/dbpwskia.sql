-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 14 Jul 2021 pada 19.32
-- Versi server: 10.4.18-MariaDB
-- Versi PHP: 8.0.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbpwskia`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `admin`
--

CREATE TABLE `admin` (
  `id` int(11) NOT NULL,
  `nama` varchar(64) NOT NULL,
  `username` varchar(32) NOT NULL,
  `nip` varchar(32) NOT NULL,
  `password` varchar(255) NOT NULL,
  `super` varchar(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `admin`
--

INSERT INTO `admin` (`id`, `nama`, `username`, `nip`, `password`, `super`) VALUES
(3, 'Karningsih, Amd.Keb', 'karningsih', '19810517 200212 2004', '65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5', '0'),
(4, 'Alfian Widy Reksa', 'alfian', '17.11.0236', '65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5', '1'),
(14, 'Abi F', 'abif', '17.11.0307', '604C163BBAD862063B4F6F0A7F949E4C404A87161A33A5A7993967E7C0B74DA1', '0');

-- --------------------------------------------------------

--
-- Struktur dari tabel `daftarjenis`
--

CREATE TABLE `daftarjenis` (
  `id` int(11) NOT NULL,
  `jenis` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `daftarjenis`
--

INSERT INTO `daftarjenis` (`id`, `jenis`) VALUES
(1, 'Kunjungan 1'),
(2, 'Kunjungan 4'),
(3, 'Kunjungan 6'),
(4, 'KF'),
(5, 'Persalinan Nakes'),
(6, 'Deteksi Risiko Nakes'),
(7, 'Deteksi Risiko Masyarakat');

-- --------------------------------------------------------

--
-- Struktur dari tabel `datapwskia`
--

CREATE TABLE `datapwskia` (
  `id` int(11) NOT NULL,
  `desa` varchar(64) NOT NULL,
  `idJenis` int(11) NOT NULL,
  `tanggal` varchar(64) NOT NULL,
  `bumil` int(11) NOT NULL,
  `bulin` int(11) NOT NULL,
  `bumilRisti` int(11) NOT NULL,
  `jmlBulanLalu` int(11) NOT NULL,
  `jmlBulanIni` int(11) NOT NULL,
  `abs` int(11) NOT NULL,
  `persentase` float NOT NULL,
  `penanggungJawab` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `datapwskia`
--

INSERT INTO `datapwskia` (`id`, `desa`, `idJenis`, `tanggal`, `bumil`, `bulin`, `bumilRisti`, `jmlBulanLalu`, `jmlBulanIni`, `abs`, `persentase`, `penanggungJawab`) VALUES
(1, 'KARANGTAWANG', 1, '1 Januari 2017', 116, 110, 23, 0, 7, 7, 6.03448, 'Karningsih, Amd.Keb'),
(2, 'KARANGPAKIS', 1, '1 Januari 2017', 110, 115, 22, 0, 19, 19, 17.2727, 'Karningsih, Amd.Keb'),
(3, 'BANJARSARI', 1, '1 Januari 2017', 72, 68, 14, 0, 8, 8, 11.1111, 'Karningsih, Amd.Keb'),
(4, 'JETIS', 1, '1 Januari 2017', 168, 161, 34, 0, 22, 22, 13.0952, 'Karningsih, Amd.Keb'),
(5, 'BANJAREJA', 1, '1 Januari 2017', 94, 89, 19, 0, 7, 7, 7.44681, 'Karningsih, Amd.Keb'),
(6, 'KARANGSEMBUNG', 1, '1 Januari 2017', 69, 66, 14, 0, 13, 13, 18.8406, 'Karningsih, Amd.Keb'),
(7, 'PURWADADI', 1, '1 Januari 2017', 40, 38, 8, 0, 5, 5, 12.5, 'Karningsih, Amd.Keb'),
(9, 'KARANGTAWANG', 2, '1 Januari 2017', 116, 110, 23, 0, 5, 5, 4.31035, 'Karningsih, Amd.Keb'),
(10, 'KARANGPAKIS', 2, '1 Januari 2017', 110, 115, 22, 0, 13, 13, 11.8182, 'Karningsih, Amd.Keb'),
(11, 'BANJARSARI', 2, '1 Januari 2017', 72, 68, 14, 0, 4, 4, 5.55555, 'Karningsih, Amd.Keb'),
(12, 'JETIS', 2, '1 Januari 2017', 168, 161, 34, 0, 9, 9, 5.35714, 'Karningsih, Amd.Keb'),
(13, 'BANJAREJA', 2, '1 Januari 2017', 94, 89, 19, 0, 2, 2, 2.12766, 'Karningsih, Amd.Keb'),
(14, 'KARANGSEMBUNG', 2, '1 Januari 2017', 69, 66, 14, 0, 6, 6, 8.69565, 'Karningsih, Amd.Keb'),
(15, 'PURWADADI', 2, '1 Januari 2017', 40, 38, 8, 0, 3, 3, 7.5, 'Karningsih, Amd.Keb'),
(17, 'KARANGTAWANG', 1, '1 Februari 2017', 116, 110, 23, 7, 7, 14, 12.069, 'Karningsih, Amd.Keb'),
(18, 'KARANGTAWANG', 2, '1 Februari 2017', 116, 110, 23, 5, 11, 16, 13.7931, 'Karningsih, Amd.Keb'),
(26, 'KARANGTAWANG', 6, '1 Januari 2017', 116, 110, 23, 0, 2, 2, 1.72414, 'Karningsih, Amd.Keb'),
(27, 'KARANGTAWANG', 7, '1 Januari 2017', 116, 110, 23, 0, 2, 2, 1.72414, 'Karningsih, Amd.Keb'),
(28, 'KARANGTAWANG', 5, '1 Januari 2017', 116, 110, 23, 0, 7, 7, 6.36364, 'Karningsih, Amd.Keb'),
(29, 'KARANGTAWANG', 4, '1 Januari 2017', 116, 110, 23, 0, 10, 10, 9.09091, 'Karningsih, Amd.Keb'),
(42, 'KARANGPAKIS', 2, '1 Februari 2017', 110, 115, 22, 13, 13, 26, 23.6364, 'Karningsih, Amd.Keb'),
(43, 'BANJARSARI', 2, '1 Februari 2017', 72, 68, 14, 4, 9, 13, 18.0556, 'Karningsih, Amd.Keb'),
(44, 'JETIS', 2, '1 Februari 2017', 168, 161, 34, 9, 10, 19, 11.3095, 'Karningsih, Amd.Keb'),
(45, 'BANJAREJA', 2, '1 Februari 2017', 94, 89, 19, 2, 3, 5, 5.31915, 'Karningsih, Amd.Keb'),
(46, 'KARANGSEMBUNG', 2, '1 Februari 2017', 69, 66, 14, 6, 6, 12, 17.3913, 'Karningsih, Amd.Keb'),
(47, 'PURWADADI', 2, '1 Februari 2017', 40, 38, 8, 3, 4, 7, 17.5, 'Karningsih, Amd.Keb'),
(54, 'NUSAWANGKAL', 1, '1 Januari 2017', 29, 27, 6, 0, 4, 4, 13.7931, 'Karningsih, Amd.Keb'),
(55, 'KARANGPAKIS', 1, '1 Februari 2017', 110, 115, 22, 19, 8, 27, 24.5455, 'Karningsih, Amd.Keb'),
(56, 'BANJARSARI', 1, '1 Februari 2017', 72, 68, 14, 8, 6, 14, 19.4444, 'Karningsih, Amd.Keb'),
(57, 'JETIS', 1, '1 Februari 2017', 168, 161, 34, 22, 14, 36, 21.4286, 'Karningsih, Amd.Keb'),
(58, 'BANJAREJA', 1, '1 Februari 2017', 94, 89, 19, 7, 12, 19, 20.2128, 'Karningsih, Amd.Keb'),
(59, 'KARANGSEMBUNG', 1, '1 Februari 2017', 69, 66, 14, 13, 2, 15, 21.7391, 'Karningsih, Amd.Keb'),
(61, 'NUSAWANGKAL', 1, '1 Februari 2017', 29, 27, 6, 4, 2, 6, 20.6897, 'Karningsih, Amd.Keb'),
(62, 'NUSAWANGKAL', 2, '1 Januari 2017', 29, 27, 6, 0, 2, 2, 6.89655, 'Karningsih, Amd.Keb'),
(63, 'NUSAWANGKAL', 2, '1 Februari 2017', 29, 27, 6, 2, 5, 7, 24.1379, 'Karningsih, Amd.Keb');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbdesa`
--

CREATE TABLE `tbdesa` (
  `id` int(11) NOT NULL,
  `nama` varchar(64) NOT NULL,
  `bumil` int(11) NOT NULL,
  `bulin` int(11) NOT NULL,
  `bumilRisti` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbdesa`
--

INSERT INTO `tbdesa` (`id`, `nama`, `bumil`, `bulin`, `bumilRisti`) VALUES
(1, 'KARANGTAWANG', 116, 110, 23),
(2, 'KARANGPAKIS', 110, 115, 22),
(3, 'BANJARSARI', 72, 68, 14),
(4, 'JETIS', 168, 161, 34),
(5, 'BANJAREJA', 94, 89, 19),
(6, 'KARANGSEMBUNG', 69, 66, 14),
(7, 'PURWADADI', 40, 38, 8),
(8, 'NUSAWANGKAL', 29, 27, 6);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `daftarjenis`
--
ALTER TABLE `daftarjenis`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `datapwskia`
--
ALTER TABLE `datapwskia`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `tbdesa`
--
ALTER TABLE `tbdesa`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `admin`
--
ALTER TABLE `admin`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT untuk tabel `daftarjenis`
--
ALTER TABLE `daftarjenis`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT untuk tabel `datapwskia`
--
ALTER TABLE `datapwskia`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=64;

--
-- AUTO_INCREMENT untuk tabel `tbdesa`
--
ALTER TABLE `tbdesa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
